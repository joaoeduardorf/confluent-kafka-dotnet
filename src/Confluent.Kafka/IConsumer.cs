// Copyright 2018 Confluent Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// Refer to LICENSE for more information.

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka.Impl;
using Confluent.Kafka.Internal;
using Confluent.Kafka.Serialization;


namespace Confluent.Kafka
{
    /// <summary>
    ///     Defines a high-level Apache Kafka consumer (with key and 
    ///     value deserialization).
    /// </summary>
    public interface IConsumer<TKey, TValue> : IClient
    {
        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.MemberId" />
        /// </summary>
        /// <value></value>
        string MemberId { get; }


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.Consume(TimeSpan)" />
        /// </summary>s
        ConsumeResult<TKey, TValue> Consume(TimeSpan timeout);


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.Consume(CancellationToken)" />
        /// </summary>
        ConsumeResult<TKey, TValue> Consume(CancellationToken cancellationToken);


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.ConsumeAsync(TimeSpan)" />
        /// </summary>
        Task<ConsumeResult<TKey, TValue>> ConsumeAsync(TimeSpan timeout);


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.ConsumeAsync(CancellationToken)" />
        /// </summary>
        Task<ConsumeResult<TKey, TValue>> ConsumeAsync(CancellationToken cancellationToken);


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.OnPartitionAssignmentReceived" />
        /// </summary>
        event EventHandler<List<TopicPartition>> OnPartitionAssignmentReceived;


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.OnPartitionAssignmentRevoked" />
        /// </summary>
        event EventHandler<List<TopicPartition>> OnPartitionAssignmentRevoked;


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.OnOffsetsCommitted" />
        /// </summary>
        event EventHandler<CommittedOffsets> OnOffsetsCommitted;


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.Assignment" />
        /// </summary>
        List<TopicPartition> Assignment { get; }


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.Subscription" />
        /// </summary>
        List<string> Subscription { get; }


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.Subscribe(IEnumerable{string})" />
        /// </summary>
        void Subscribe(IEnumerable<string> topics);


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.Subscribe(string)" />
        /// </summary>
        /// <param name="topic"></param>
        void Subscribe(string topic);
        

        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.Unsubscribe" />
        /// </summary>
        void Unsubscribe();


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.Assign(TopicPartition)" />
        /// </summary>
        /// <param name="partition"></param>
        void Assign(TopicPartition partition);


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.Assign(TopicPartitionOffset)" />
        /// </summary>
        /// <param name="partition"></param>
        void Assign(TopicPartitionOffset partition);


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.Assign(IEnumerable{TopicPartitionOffset})" />
        /// </summary>
        void Assign(IEnumerable<TopicPartitionOffset> partitions);


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.Assign(TopicPartition)" />
        /// </summary>
        void Assign(IEnumerable<TopicPartition> partitions);


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.Unassign" />
        /// </summary>
        void Unassign();


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.StoreOffset(ConsumeResult{TKey, TValue})" />
        /// </summary>
        TopicPartitionOffset StoreOffset(ConsumeResult<TKey, TValue> result);


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.StoreOffsets(IEnumerable{TopicPartitionOffset})" />
        /// </summary>
        List<TopicPartitionOffset> StoreOffsets(IEnumerable<TopicPartitionOffset> offsets);


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.CommitAsync(CancellationToken)" />
        /// </summary>
        Task<List<TopicPartitionOffset>> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.CommitAsync(ConsumeResult{TKey, TValue}, CancellationToken)" />
        /// </summary>
        Task<List<TopicPartitionOffset>> CommitAsync(ConsumeResult<TKey, TValue> result, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.CommitAsync(IEnumerable{TopicPartitionOffset}, CancellationToken)" />
        /// </summary>
        Task<List<TopicPartitionOffset>> CommitAsync(IEnumerable<TopicPartitionOffset> offsets, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.Seek(TopicPartitionOffset)" />
        /// </summary>
        void Seek(TopicPartitionOffset tpo);


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.Pause(IEnumerable{TopicPartition})" />
        /// </summary>
        void Pause(IEnumerable<TopicPartition> partitions);


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.Resume(IEnumerable{TopicPartition})" />
        /// </summary>
        void Resume(IEnumerable<TopicPartition> partitions);


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.CommittedAsync(IEnumerable{TopicPartition}, TimeSpan, CancellationToken)" />
        /// </summary>
        Task<List<TopicPartitionOffset>> CommittedAsync(IEnumerable<TopicPartition> partitions, TimeSpan timeout, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.PositionAsync(IEnumerable{TopicPartition})" />
        /// </summary>
        Task<List<TopicPartitionOffset>> PositionAsync(IEnumerable<TopicPartition> partitions);


        /// <summary>
        ///     Refer to <see cref="Confluent.Kafka.Consumer{TKey, TValue}.OffsetsForTimesAsync(IEnumerable{TopicPartitionTimestamp}, TimeSpan, CancellationToken)" />
        /// </summary>
        Task<List<TopicPartitionOffset>> OffsetsForTimesAsync(IEnumerable<TopicPartitionTimestamp> timestampsToSearch, TimeSpan timeout, CancellationToken cancellationToken = default(CancellationToken));
    }
}