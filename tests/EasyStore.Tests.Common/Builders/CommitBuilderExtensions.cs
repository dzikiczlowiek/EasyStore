namespace EasyStore.Tests.Common.Builders
{
    using System;

    using EasyObjectBuilder;

    using EasyStore.Persistence;

    public static class CommitBuilderExtensions
    {
        public static ISingleObjectBuilder<Commit> CreateStandard(
            this ISingleObjectBuilder<Commit> builder,
            Guid commitId,
            int commitSequence,
            int streamRevision)
        {
            builder.InitializeWith(() => new Commit(commitId, commitSequence, streamRevision, null));
            return builder;
        }

        public static ISingleObjectBuilder<ICommit> CreateStandard(
            this ISingleObjectBuilder<ICommit> builder,
            Func<Guid> commitId,
            Func<int> commitSequence,
            Func<int> streamRevision)
        {
            builder.InitializeWith(() => new Commit(commitId(), commitSequence(), streamRevision(), null));
            return builder;
        }
    }
}
