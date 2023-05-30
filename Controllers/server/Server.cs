using AlbyLib;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AlbyAirLines.Controllers
{
    public class Server
    {
        private SocketServer server = new SocketServer(15, 5000);
        private AlbySqlControllerMultiple sqlController;

        public delegate void ErrorDelegate(string message);
        public event ErrorDelegate Error;

        private string DbPath = "";

        public Server(string path)
        {
            DbPath = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"{path}\\DataBase\\AAL_DB.mdf\";Integrated Security=True;Connect Timeout=30";
        }

        public void Start()
        {
            try
            {
                sqlController = new AlbySqlControllerMultiple(DbPath);

                server.ClientConnected += HandleResponse;
                server.Error += HandleError;

                server.Start();
            }
            catch (Exception ex)
            {
                if (Error != null)
                    Error(ex.Message);
            }
        }

        private string HandleResponse(string received)
        {
            string ritorno;

            if (received == "idRequest")
            {
                return GetNewId();
            }

            AirPlaneClientModel apc = JsonConvert.DeserializeObject<AirPlaneClientModel>(received);

            if (apc == null)
            {
                return "400";
            }

            if (!IsIdInDb(apc.Id))
            {
                sqlController.Query("INSERT INTO live_air_traffic (" +
                                     "Id, longitude, latitude, plane_name, plane_type)" +
                                     "VALUES (@id, @longitude, @latitude, @plane_name, @plane_type)",
                    new SqlParameter[]{
                        new SqlParameter("@id", SqlDbType.VarChar, 15) { Value = apc.Id} ,
                        new SqlParameter("@longitude", SqlDbType.Float) { Value = apc.Longitude},
                        new SqlParameter("@latitude", SqlDbType.Float) { Value = apc.Latitude},
                        new SqlParameter("@plane_name", SqlDbType.VarChar, 30) { Value = apc.Name},
                        new SqlParameter("@plane_type", SqlDbType.Char, 1) { Value = apc.Type}
                    },
                    QueryType.NonQuery
                );
            }
            else
            {
                sqlController.Query("UPDATE live_air_traffic SET " +
                                     "Id = @id, longitude = @longitude, latitude = @latitude," +
                                     " plane_name = @plane_name, plane_type = @plane_type " +
                                     "WHERE Id = @id",
                    new SqlParameter[]{
                        new SqlParameter("@id", SqlDbType.VarChar, 15) { Value = apc.Id} ,
                        new SqlParameter("@longitude", SqlDbType.Float) { Value = apc.Longitude},
                        new SqlParameter("@latitude", SqlDbType.Float) { Value = apc.Latitude},
                        new SqlParameter("@plane_name", SqlDbType.VarChar, 30) { Value = apc.Name},
                        new SqlParameter("@plane_type", SqlDbType.Char, 1) { Value = apc.Type}
                    },
                    QueryType.NonQuery
                );
            }

            ritorno = "200<EOF>";

            return ritorno;
        }

        public void Close()
        {
            server.Close();
        }

        private void HandleError(string message)
        {
            if (message != "Thread interrotto." && Error != null)
                Error(message);
        }

        private string GetNewId()
        {
            Random rnd = new Random();
            string newId;

            do
            {
                newId = "";

                for (int i = 0; i < 15; i++)
                    newId += Convert.ToChar(rnd.Next(0, 26) + (rnd.Next(0, 100) < 50 ? 65 : 97));

            } while (IsIdInDb(newId));

            return newId + "<EOF>";
        }

        private bool IsIdInDb(string id)
        {
            return (int)sqlController.Query("SELECT count(*) FROM live_air_traffic WHERE Id = @newId",
                    new SqlParameter[]
                    {
                        new SqlParameter("@newId", SqlDbType.VarChar, 15) { Value = id }
                    },
                    QueryType.Scalar) > 0;
        }

        public DataTable GetLivePositions()
        {
            DataTable dt = null;

            try
            {
                dt = (DataTable)sqlController.Query("SELECT * FROM live_air_traffic");
            }
            catch (Exception ex)
            {
                if (Error != null)
                    Error(ex.Message);
            }

            return dt;
        }

        public void ClearLivePositions()
        {
            try
            {
                sqlController.Query("delete from live_air_traffic", QueryType.NonQuery);
            }
            catch (Exception ex)
            {
                if (Error != null)
                    Error(ex.Message);
            }
        }
    }
}