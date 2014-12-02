using System;
using System.ComponentModel;

namespace MsCrmTools.MetadataBrowser.AppCode.OneToManyRelationship
{
    /// <summary>
    ///     Summary description for CollectionPropertyDescriptor.
    /// </summary>
    public class OneToManyRelationshipCollectionPropertyDescriptor : PropertyDescriptor
    {
        private readonly OneToManyRelationshipCollection collection;
        private readonly int index = -1;

        public OneToManyRelationshipCollectionPropertyDescriptor(OneToManyRelationshipCollection coll, int idx) :
            base("#" + idx, null)
        {
            collection = coll;
            index = idx;
        }

        public override AttributeCollection Attributes
        {
            get { return new AttributeCollection(null); }
        }

        public override Type ComponentType
        {
            get { return collection.GetType(); }
        }

        public override string DisplayName
        {
            get
            {
                OneToManyRelationshipMetadataInfo rmi = collection[index];
                return rmi.SchemaName;
            }
        }

        public override string Description
        {
            get
            {
                OneToManyRelationshipMetadataInfo rmi = collection[index];
                return rmi.SchemaName;
            }
        }

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override string Name
        {
            get { return "#" + index; }
        }

        public override Type PropertyType
        {
            get { return collection[index].GetType(); }
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override object GetValue(object component)
        {
            return collection[index];
        }

        public override void ResetValue(object component)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        public override void SetValue(object component, object value)
        {
            // collection[index] = value;
        }
    }
}