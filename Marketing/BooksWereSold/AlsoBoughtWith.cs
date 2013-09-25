using Neo4jClient;

namespace Marketing.BooksWereSold
{
    public class AlsoBoughtWith : Relationship<AlsoBoughtWith.TimesBought>,
         IRelationshipAllowingSourceNode<Book>,
         IRelationshipAllowingTargetNode<Book>
    {
        public AlsoBoughtWith(NodeReference targetNode, TimesBought data)
            : base(targetNode, data)
        {}

        public AlsoBoughtWith() : base(null, null) { }

        public const string TypeKey = "BOOK_ALSO_BOUGHT_WITH";

        public override string RelationshipTypeKey
        {
            get { return TypeKey; }
        }

        public class TimesBought
        {
            public int Count { get; set; }
        }
    }
}