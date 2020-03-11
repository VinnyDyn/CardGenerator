using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using VinnyB.CardGenerator.Extends;
using VinnyB.CardGenerator.Models;

namespace VinnyB.CardGenerator.Business
{
    /// <summary>
    /// Card Generator for CDS Records
    /// </summary>
    public class CardGeneratorBusiness
    {
        private IOrganizationService Service;
        private IOrganizationService ServiceAdmin;

        public CardGeneratorBusiness(IOrganizationService service, IOrganizationService serviceAdmin)
        {
            this.Service = service;
            this.ServiceAdmin = serviceAdmin;
        }

        /// <summary>
        /// Generate a Card
        /// </summary>
        /// <param name="entityName">Entity Logical Name</param>
        /// <param name="entityId">Entity Id</param>
        /// <param name="appId">App Id</param>
        /// <param name="attributes">Attributes</param>
        /// <returns></returns>
        public string ConvertToCard(string entityName, Guid entityId, Guid appId, string[] attributes)
        {
            //Retrieve Entity Record
            var entity = this.RetrieveRecord(entityName, entityId, attributes);

            //Retrieve Attributes Metadata
            var attributesMetadata = this.RetrieveAttributes(entityName, attributes);

            //Card
            var adaptativeCard = new AdaptativeCard();
            {
                //Button
                var bodyAction = new BodyAction();
                {
                    var action = new Models.Action("Open Record", this.DefineRecordUrl(appId, entity));
                    bodyAction.actions.Add(action);
                }
                adaptativeCard.body.Add(bodyAction);
            }

            //Facts
            var bodyFact = new BodyFact();
            {
                #region Facts
                for (int i = 0; i < attributes.Length; i++)
                {
                    object value = null;
                    if (entity.Contains(attributes[i]))
                        value = entity.Attributes[attributes[i]];
                    else
                        value = string.Empty;

                    //Fact
                    var match = attributesMetadata.Where(w => w.LogicalName == attributes[i]).FirstOrDefault();
                    if (match != null)
                        bodyFact.facts.Add(new Fact(match, value));
                }
                #endregion
            }
            adaptativeCard.body.Add(bodyFact);

            return adaptativeCard.CardToJson();
        }

        /// <summary>
        /// Retrieve Record
        /// </summary>
        /// <param name="entityName">Entity Logical Name</param>
        /// <param name="entityId">Entity Id</param>
        /// <param name="attributes">Attributes</param>
        /// <returns></returns>
        private Entity RetrieveRecord(string entityName, Guid entityId, string[] attributes)
        {
            return ServiceAdmin.Retrieve(entityName, entityId, new ColumnSet(attributes));
        }

        /// <summary>
        /// Retrieve Metadata properties based on RetrieveEntityRequest
        /// </summary>
        /// <param name="entityName">Entity Logical Name</param>
        /// <param name="attributes">String Attributes[]</param>
        /// <returns></returns>
        private List<AttributeCardModel> RetrieveAttributes(string entityName, string[] attributes)
        {
            List<AttributeCardModel> attributeCards = new List<AttributeCardModel>();

            RetrieveEntityRequest retrieveEntityRequest = new RetrieveEntityRequest()
            {
                LogicalName = entityName,
                EntityFilters = EntityFilters.Attributes,
                RetrieveAsIfPublished = true
            };

            RetrieveEntityResponse retrieveEntityResponse = (RetrieveEntityResponse)ServiceAdmin.Execute(retrieveEntityRequest);
            EntityMetadata entityMetadata = retrieveEntityResponse.EntityMetadata;

            foreach (var attributeMetadata_ in entityMetadata.Attributes)
            {
                var attributeMetadata = (AttributeMetadata)attributeMetadata_;
                var match = attributes.Where(w => w.Equals(attributeMetadata.LogicalName)).FirstOrDefault();
                if (!String.IsNullOrEmpty(match))
                    attributeCards.Add(attributeMetadata.ToCard());
            }

            return attributeCards;
        }

        /// <summary>
        /// Generate a Record URL based on return of RetrieveCurrentOrganizationRequest
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="entity">Entity Id</param>
        /// <returns></returns>
        private string DefineRecordUrl(Guid appId, Entity entity)
        {
            RetrieveCurrentOrganizationRequest retrieveCurrentOrganizationRequest = new RetrieveCurrentOrganizationRequest()
            {
                AccessType = Microsoft.Xrm.Sdk.Organization.EndpointAccessType.Default,
            };
            var retrieveCurrentOrganizationResponse = (RetrieveCurrentOrganizationResponse)ServiceAdmin.Execute(retrieveCurrentOrganizationRequest);
            string absoluteUrl = retrieveCurrentOrganizationResponse.Detail.Endpoints[Microsoft.Xrm.Sdk.Organization.EndpointType.WebApplication];

            if (appId != Guid.Empty)
                return $"{absoluteUrl}main.aspx?appid={appId.ToString()}&pagetype=entityrecord&etn={entity.LogicalName}&id={entity.Id}";
            else
                return $"{absoluteUrl}main.aspx?app=d365default&pagetype=entityrecord&etn={entity.LogicalName}&id={entity.Id}";
        }
    }
}
