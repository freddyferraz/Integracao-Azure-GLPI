using Newtonsoft.Json;

namespace Integracao.Domain.ValueObjects.Azure;
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public record Avatar(
string href
);

public record Fields(
    [property: JsonProperty("System.AreaPath")] string SystemAreaPath,
    [property: JsonProperty("System.TeamProject")] string SystemTeamProject,
    [property: JsonProperty("System.IterationPath")] string SystemIterationPath,
    [property: JsonProperty("System.WorkItemType")] string SystemWorkItemType,
    [property: JsonProperty("System.State")] string SystemState,
    [property: JsonProperty("System.Reason")] string SystemReason,
    [property: JsonProperty("System.CreatedDate")] DateTime SystemCreatedDate,
    [property: JsonProperty("System.CreatedBy")] SystemCreatedBy SystemCreatedBy,
    [property: JsonProperty("System.ChangedDate")] DateTime SystemChangedDate,
    [property: JsonProperty("System.ChangedBy")] SystemChangedBy SystemChangedBy,
    [property: JsonProperty("System.CommentCount")] int SystemCommentCount,
    [property: JsonProperty("System.Title")] string SystemTitle,
    [property: JsonProperty("System.BoardColumn")] string SystemBoardColumn,
    [property: JsonProperty("System.BoardColumnDone")] bool SystemBoardColumnDone,
    [property: JsonProperty("Microsoft.VSTS.Common.StateChangeDate")] DateTime MicrosoftVSTSCommonStateChangeDate,
    [property: JsonProperty("Microsoft.VSTS.Common.Priority")] int MicrosoftVSTSCommonPriority,
    [property: JsonProperty("Microsoft.VSTS.Common.Severity")] string MicrosoftVSTSCommonSeverity,
    [property: JsonProperty("Microsoft.VSTS.Common.ValueArea")] string MicrosoftVSTSCommonValueArea,
    [property: JsonProperty("WEF_E0FFEA61E07D47799239883C4006489A_Kanban.Column")] string WEF_E0FFEA61E07D47799239883C4006489A_KanbanColumn,
    [property: JsonProperty("WEF_E0FFEA61E07D47799239883C4006489A_Kanban.Column.Done")] bool WEF_E0FFEA61E07D47799239883C4006489A_KanbanColumnDone,
    [property: JsonProperty("Custom.TipodeChamado")] string CustomTipodeChamado,
    [property: JsonProperty("Custom.CategoriadoChamado")] string CustomCategoriadoChamado,
    [property: JsonProperty("Custom.25d8d63f-ed8b-42fc-b72a-754868bbdcac")] string Custom25d8d63fed8b42fcb72a754868bbdcac,
    [property: JsonProperty("Custom.Impacto")] string CustomImpacto,
    [property: JsonProperty("Custom.Prioridade")] string CustomPrioridade,
    [property: JsonProperty("Custom.fb57db61-fbe4-4ec1-9a1a-822f31903c9a")] string Customfb57db61fbe44ec19a1a822f31903c9a,
    [property: JsonProperty("Custom.LinkparaoChamado")] string CustomLinkparaoChamado,
    [property: JsonProperty("Custom.c632119c-2c55-43ba-9da6-86e59ab7e5c8")] DateTime Customc632119c2c5543ba9da686e59ab7e5c8,
    [property: JsonProperty("WEF_99A7A3D4896B4CB8A819DA9F5660278E_Kanban.Column")] string WEF_99A7A3D4896B4CB8A819DA9F5660278E_KanbanColumn,
    [property: JsonProperty("WEF_99A7A3D4896B4CB8A819DA9F5660278E_Kanban.Column.Done")] bool WEF_99A7A3D4896B4CB8A819DA9F5660278E_KanbanColumnDone,
    [property: JsonProperty("WEF_DB457BA9D40E4360BF7CBA9DCF5D60A2_Kanban.Column")] string WEF_DB457BA9D40E4360BF7CBA9DCF5D60A2_KanbanColumn,
    [property: JsonProperty("WEF_DB457BA9D40E4360BF7CBA9DCF5D60A2_Kanban.Column.Done")] bool WEF_DB457BA9D40E4360BF7CBA9DCF5D60A2_KanbanColumnDone,
    [property: JsonProperty("Custom.Requerente")] string CustomRequerente,
    [property: JsonProperty("Custom.Observador")] string CustomObservador,
    [property: JsonProperty("System.Description")] string SystemDescription,
string href
);

public record Html(
string href
);

public record Links(
Avatar avatar,
Self self,
WorkItemUpdates workItemUpdates,
WorkItemRevisions workItemRevisions,
WorkItemComments workItemComments,
Html html,
WorkItemType workItemType,
Fields fields
);

public record CardAzureResponse(
int id,
int rev,
Fields fields,
Links _links,
string url
);

public record Self(
string href
);

public record SystemChangedBy(
string displayName,
string url,
Links _links,
string id,
string uniqueName,
string imageUrl,
string descriptor
);

public record SystemCreatedBy(
string displayName,
string url,
Links _links,
string id,
string uniqueName,
string imageUrl,
string descriptor
);

public record WorkItemComments(
string href
);

public record WorkItemRevisions(
string href
);

public record WorkItemType(
string href
);

public record WorkItemUpdates(
string href
);
