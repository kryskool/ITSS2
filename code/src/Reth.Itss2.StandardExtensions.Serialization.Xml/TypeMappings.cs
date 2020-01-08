using Reth.Itss2.StandardExtensions.Dialogs;
using Reth.Itss2.StandardExtensions.Dialogs.ArticleData.ArticleMasterSet;
using Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet;
using Reth.Itss2.StandardExtensions.Serialization.Xml.DataContracts;
using Reth.Itss2.StandardExtensions.Serialization.Xml.DataContracts.ArticleData.ArticleMasterSet;
using Reth.Itss2.StandardExtensions.Serialization.Xml.DataContracts.Storage.ConfigurationGet;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.StandardExtensions.Serialization.Xml
{
    public class TypeMappings:Reth.Itss2.Standard.Serialization.Xml.TypeMappings
    {
        public TypeMappings()
        :
            base( typeof( TypeMappings ) )
        {
            base.Register( typeof( TypeMappings ), typeof( ArticleMasterSetArticleDataContract<> ), typeof( ArticleMasterSetArticle ) );
            base.Register( typeof( TypeMappings ), typeof( ArticleMasterSetRequestDataContract<> ), typeof( ArticleMasterSetRequest ) );
            base.Register( typeof( TypeMappings ), typeof( DialogNameDataContract<> ), typeof( IDialogName ), typeof( DialogName ) );

            base.Register( typeof( TypeMappings ), typeof( ConfigurationDataContract<> ), typeof( Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet.Configuration ) );
            base.Register( typeof( TypeMappings ), typeof( ConfigurationGetRequestDataContract<> ), typeof( ConfigurationGetRequest ) );
            base.Register( typeof( TypeMappings ), typeof( ConfigurationGetResponseDataContract<> ), typeof( ConfigurationGetResponse ) );
        }
    }
}