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
            int commitSequence)
        {
            builder.InitializeWith(() => new Commit(commitId, commitSequence, null));
            return builder;
        }
    }
}
