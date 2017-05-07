namespace Model
{
    public class CollectionPointDetailsBO
    {
        private string collectionPointId;
        private string collectionPoint;
        private string collectionTime;

        public string CollectionPointId
        {
            get
            {
                return collectionPointId;
            }

            set
            {
                collectionPointId = value;
            }
        }

        public string CollectionPoint
        {
            get
            {
                return collectionPoint;
            }

            set
            {
                collectionPoint = value;
            }
        }

        public string CollectionTime
        {
            get
            {
                return collectionTime;
            }

            set
            {
                collectionTime = value;
            }
        }
    }
}
