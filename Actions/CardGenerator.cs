using Microsoft.Xrm.Sdk;
using System;
using VinnyB.CardGenerator.Business;

namespace VinnyB.CardGenerator.Actions
{
    public class CardGenerator : PluginBase
    {
        public CardGenerator() : base(typeof(CardGenerator)) { }

        /// <summary>
        /// Classe principal que chama os métodos.
        /// </summary>
        /// <param name="localContext"> Contexto de execução. </param>

        protected override void ExecuteCrmPlugin(LocalPluginContext localContext)
        {
            if (localContext == null) { throw new InvalidPluginExecutionException("Contexto não localizado!"); };

            try
            {
                //App Id
                string appId_ = (string)localContext.PluginExecutionContext.InputParameters["AppId"];
                appId_ = !String.IsNullOrEmpty(appId_) ? appId_ : Guid.Empty.ToString();

                //Entity Logical Name
                string entityName = (string)localContext.PluginExecutionContext.InputParameters["EntityName"];

                //Entity Id
                string entityId_ = (string)localContext.PluginExecutionContext.InputParameters["EntityId"];

                //Attributes Splited by comma ','
                string attributes_ = (string)localContext.PluginExecutionContext.InputParameters["EntityAttributes"];                

                Guid appId = Guid.Empty;
                Guid entityId = Guid.Empty;
                if (Guid.TryParse(appId_, out appId) && Guid.TryParse(entityId_, out entityId))
                {
                    CardGeneratorBusiness bo = new CardGeneratorBusiness(localContext.OrganizationService, localContext.OrganizationServiceAdmin);
                    string card = bo.ConvertToCard(entityName, entityId, Guid.Empty, attributes_.Split(','));
                    localContext.PluginExecutionContext.OutputParameters["Sucess"] = true;
                    localContext.PluginExecutionContext.OutputParameters["Trace"] = string.Empty;
                    localContext.PluginExecutionContext.OutputParameters["Card"] = card;
                }
                else
                {
                    localContext.PluginExecutionContext.OutputParameters["Sucess"] = false;
                    localContext.PluginExecutionContext.OutputParameters["Trace"] = $"InvalidCastException String to Guid {appId_} | {entityId_}";
                    localContext.PluginExecutionContext.OutputParameters["Card"] = "{}";
                }
            }
            catch(InvalidCastException ice)
            {
                throw ice;
            }
            catch(InvalidPluginExecutionException ipee)
            {
                throw ipee;
            }
            catch(Exception e)
            {
                localContext.PluginExecutionContext.OutputParameters["Sucess"] = true;
                localContext.PluginExecutionContext.OutputParameters["Trace"] = e.Message;
                localContext.PluginExecutionContext.OutputParameters["Card"] = "{}";
            }
        }
    }
}