using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Classe réprésentant une entité secrétaire
    /// </summary>
    public class Secretaire:Utilisateur
    {
        private string _service;
        private int _idSecretaire;

       
        #region Properties
        /// <summary>
        /// Permet de renseigner le service de la secrétaire
        /// </summary>
        public string Service
        {
            get { return _service; }
            set { _service = value; }
        }
        /// <summary>
        /// Identifiant de la secrétaire
        /// </summary>
        public int IdSecretaire
        {
            get { return _idSecretaire; }
            set { _idSecretaire = value; }
        }
        #endregion
        /// <summary>
        /// Permet de récupérer la secrétaire de la DB via son id
        /// </summary>
        /// <param name="id">Identifiant de la secrétaire</param>
        /// <returns>Une secrétaire ou null si l'identifiant n'existe pas</returns>
        #region Function
        public static Secretaire getInfo(int id)
        {
            List<Dictionary<string, object>> infoUser = GestionConnexion.Instance.getData("Select * from Secretaire where idSecretaire=" + id);
            Secretaire retour = new Secretaire();
            foreach (Dictionary<string, object> item in infoUser)
            {
                Utilisateur.getInfo(item["FKidUtilisateur"].ToString(), retour as Utilisateur);
                retour.IdSecretaire = id;
                retour.Service = item["service"].ToString();
            }
            return retour;
        }

        /// <summary>
        /// Permet de sauvegarder la secretaire dans la DB
        /// <seealso cref="Utilisateur.saveMe"/>
        /// </summary>
        /// <returns>true si la secretaire a pu être enregistré</returns>
        public override bool saveMe()
        {

            if (base.saveMe()) //Appel de l'enregistrement du parent (Utilisateur)... si el parent s'enregistre => poursuite
            {
                //Requête parametrée
                string query = @"INSERT INTO [MedicoDB].[dbo].[Secretaire]
                                   ([service]
                                   ,[FKidUtilisateur])
                             VALUES
                                   (@service,@FKIdUtilisateur)";

                //les données a insérer dans un dictionnaire
                Dictionary<string, object> valeurs = new Dictionary<string, object>();
                valeurs.Add("service", this.Service);
                valeurs.Add("FKIdUtilisateur", this.IdUtilisateur); //idUtilisateur est récupéré du parent qui a été préalablement enregistré

                //Sauvegarde via la classe globale de gestion de données
                if (GestionConnexion.Instance.saveData(query, GenerateKey.APP, valeurs))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Permet de récupérer toutes les secrétaire de la DB classée par services
        /// </summary> 
        /// <returns>Une secrétaire ou null si l'identifiant n'existe pas</returns>
        public static List<Secretaire> getInfos()
        {
            List<Dictionary<string, object>> infoUser = GestionConnexion.Instance.getData("Select * from Secretaire order by service  ");
            List<Secretaire> retour = new List<Secretaire>();
            foreach (Dictionary<string, object> item in infoUser)
            {

            }
            return retour;
        } 
        #endregion
    }
}
