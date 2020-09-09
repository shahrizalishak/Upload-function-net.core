using Abp.Dependency;
using GraphQL;
using GraphQL.Types;
using eForm.Queries.Container;

namespace eForm.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IDependencyResolver resolver) :
            base(resolver)
        {
            Query = resolver.Resolve<QueryContainer>();
        }
    }
}