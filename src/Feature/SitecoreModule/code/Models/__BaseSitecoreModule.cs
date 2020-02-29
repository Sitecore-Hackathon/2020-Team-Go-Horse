namespace Feature.SitecoreModule.Models
{
    public partial class __BaseSitecoreModule
    {
        public static class FieldNames
        {
            public static readonly string CreatedBy = "__created by";
            public static readonly string Created = "__created";
        }

        public string Created
        {
            get { return InnerItem[FieldNames.Created]; }
            set { InnerItem[FieldNames.Created] = value; }
        }

        public string CreatedBy
        {
            get { return InnerItem[FieldNames.CreatedBy].Replace(@"sitecore\", ""); }
            set { InnerItem[FieldNames.CreatedBy] = value; }
        }
    }
}