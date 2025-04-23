using Dapper;
using Integracao.Infra.Abstractions;
using Integracao.Infra.Entities;

namespace Integracao.Infra.Repositories;

public sealed class DeParaStatusRepository(IntegracaoContext dbcontext, IDbSession dbSession) : IDeParaStatusRepository
{

    public async ValueTask<TDEPARAStatus> RetornaTDeParaStatusById(long? idStatusDevOps = null, long?  idStatusGlpi = null)
    {
        var sql = @"SELECT ACOD_STATUS_GLPI AS acodStatusglpi, ADES_STATUS_GLPI as adesStatusGlpi, ACOD_STATUS_DEVOPS as acodStatusDevops, ADES_STATUS_DEVOPS as acodStatusDevops, ACOD_STATUS as acodStatus 
                     FROM TDEPARAStatus";

        long? id = 0;

        if(idStatusDevOps is not null)
        {
            sql = sql + " WHERE ACOD_STATUS_DEVOPS = @CodigoStatus";
            id = idStatusDevOps;
        }
        else if(idStatusGlpi is not null)
        {
            sql = sql + " WHERE ACOD_STATUS_GLPI = @CodigoStatus";
            id = idStatusGlpi;
        }

        var query = dbSession.Connection.QueryFirstOrDefault<TDEPARAStatus>(sql, new { CodigoStatus =  id});


        return query;

    }


}