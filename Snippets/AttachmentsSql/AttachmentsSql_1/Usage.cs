using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Attachments.Sql;

public class Usage
{
    string connectionString = null;

    Usage(EndpointConfiguration endpointConfiguration)
    {
        #region EnableAttachments

        endpointConfiguration.EnableAttachments(
            connectionFactory: async () =>
            {
                var connection = new SqlConnection(connectionString);
                try
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                    return connection;
                }
                catch
                {
                    connection.Dispose();
                    throw;
                }
            },
            timeToKeep: messageTimeToBeReceived => TimeSpan.FromDays(7));

        #endregion

        #region EnableAttachmentsRecommended

        endpointConfiguration.EnableAttachments(
            connectionFactory: OpenConnection,
            timeToKeep: TimeToKeep.Default);

        #endregion
    }

    void DisableCleanupTask(EndpointConfiguration endpointConfiguration)
    {
        #region DisableCleanupTask

        var attachments = endpointConfiguration.EnableAttachments(
            connectionFactory: OpenConnection,
            timeToKeep: TimeToKeep.Default);
        attachments.DisableCleanupTask();

        #endregion
    }

    void UseTransportConnectivity(EndpointConfiguration endpointConfiguration)
    {
        #region UseTransportConnectivity

        var attachments = endpointConfiguration.EnableAttachments(OpenConnection, TimeToKeep.Default);
        attachments.UseTransportConnectivity();

        #endregion
    }

    void UseSynchronizedStorageSessionConnectivity(EndpointConfiguration endpointConfiguration)
    {
        #region UseSynchronizedStorageSessionConnectivity

        var attachments = endpointConfiguration.EnableAttachments(OpenConnection, TimeToKeep.Default);
        attachments.UseSynchronizedStorageSessionConnectivity();

        #endregion
    }

    void ExecuteAtStartup(EndpointConfiguration endpointConfiguration)
    {
        #region ExecuteAtStartup

        endpointConfiguration.EnableInstallers();
        var attachments = endpointConfiguration.EnableAttachments(
            connectionFactory: OpenConnection,
            timeToKeep: TimeToKeep.Default);

        #endregion
    }

    void DisableInstaller(EndpointConfiguration endpointConfiguration)
    {
        #region DisableInstaller

        endpointConfiguration.EnableInstallers();
        var attachments = endpointConfiguration.EnableAttachments(
            connectionFactory: OpenConnection,
            timeToKeep: TimeToKeep.Default);
        attachments.DisableInstaller();

        #endregion
    }

    void UseTableName(EndpointConfiguration endpointConfiguration)
    {
        #region UseTableName

        var attachments = endpointConfiguration.EnableAttachments(
            connectionFactory: OpenConnection,
            timeToKeep: TimeToKeep.Default);
        attachments.UseTable(new Table("CustomAttachmentsTableName", "dbo"));

        #endregion
    }

    #region OpenConnection

    async Task<SqlConnection> OpenConnection()
    {
        var connection = new SqlConnection(connectionString);
        try
        {
            await connection.OpenAsync().ConfigureAwait(false);
            return connection;
        }
        catch
        {
            connection.Dispose();
            throw;
        }
    }

    #endregion
}