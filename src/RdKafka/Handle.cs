using System;
using System.Globalization;
using System.Runtime.InteropServices;
using RdKafka.Internal;

namespace RdKafka
{
    public class Handle
    {
        internal SafeKafkaHandle handle;
        LibRdKafka.LogCallback LogDelegate;
        LibRdKafka.StatsCallback StatsDelegate;

        internal void Init(RdKafkaType type, IntPtr config, Config.LogCallback logger)
        {
            logger = logger ?? ((string handle, int level, string fac, string buf) =>
            {
                var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                Console.WriteLine($"{level}|{now}|{handle}|{fac}| {buf}");
            });
            LogDelegate = (IntPtr rk, int level, string fac, string buf) =>
            {
                // The log_cb is called very early during construction, before
                // SafeKafkaHandle or any of the C# wrappers are ready.
                // So we can't really pass rk on, just pass the rk name instead.
                var name = Marshal.PtrToStringAnsi(SafeKafkaHandle.rd_kafka_name(rk));
                logger(name, level, fac, buf);
            };
            LibRdKafka.rd_kafka_conf_set_log_cb(config, LogDelegate);

            StatsDelegate = (IntPtr rk, IntPtr json, UIntPtr json_len, IntPtr opaque) =>
            {
                OnStatistics?.Invoke(this, Marshal.PtrToStringAnsi(json));
                return 0;
            };
            LibRdKafka.rd_kafka_conf_set_stats_cb(config, StatsDelegate);

            handle = SafeKafkaHandle.Create(type, config);
        }

        /// <summary>
        /// The name of the handle
        /// </summary>
        public string Name => handle.GetName();

        /// <summary>
        /// The client's broker-assigned group member id
        ///
        /// Last assigned member id, or empty string if not currently
        /// a group member.
        /// </summary>
        public string MemberId => handle.MemberId();

        /// <summary>
        /// The current out queue length
        ///
        /// The out queue contains messages and requests waiting to be sent to,
        /// or acknowledged by, the broker.
        /// </summary>
        public long OutQueueLength => handle.GetOutQueueLength();

        public int LogLevel
        {
            set {
                handle.SetLogLevel(value);
            }
        }

        /// <summary>
        /// Request Metadata from broker.
        ///
        /// Parameters:
        ///   allTopics    - if true: request info about all topics in cluster,
        ///                  if false: only request info about locally known topics.
        ///   onlyForTopic - only request info about this topic
        ///   includeInternal - include internal topics prefixed with __
        ///   timeout      - maximum response time before failing.
        /// </summary>
        public Metadata Metadata (bool allTopics=true, Topic onlyForTopic=null,
                bool includeInternal=false, TimeSpan timeout=default(TimeSpan))
        {
            return handle.Metadata(allTopics, onlyForTopic?.handle, includeInternal, timeout);
        }

        public event EventHandler<string> OnStatistics;
    }
}