using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additonal Namespaces
using System.ComponentModel; //ODS
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
#endregion

namespace ChinookSystem.BLL
{  [DataObject]
   public class TrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<Track> ListTracks()
        {
            using (var context = new ChinookContext())
            {
                return context.Tracks.ToList();
            }
        }

       
            [DataObjectMethod(DataObjectMethodType.Select, false)]
            public Track GetTrack(int trackid)
            {
                using (var context = new ChinookContext())
                {
                //return a record all attributes
                    return context.Tracks.Find(trackid);
                }
            }


        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddTrack(Track trackinfo)
        {
            using (var context = new ChinookContext())
            {
                //any business rules

                //any data refinements
                //review of using iff
                //composer can be a null string
                //we do not wish to store an empty string
                trackinfo.Composer = string.IsNullOrEmpty(trackinfo.Composer)?
                                     null : trackinfo.Composer;
                //add the instance of trackinfo to the database
                context.Tracks.Add(trackinfo);

                //commit the transaction 
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void UpdateTrack(Track trackinfo)
        {
            using (var context = new ChinookContext())
            {
                //any business rules

                //any data refinements
                //review of using iff
                //composer can be a null string
                //we do not wish to store an empty string
                trackinfo.Composer = string.IsNullOrEmpty(trackinfo.Composer) ?
                                     null : trackinfo.Composer;
                //update the existing the instance of trackinfo on the database
                context.Entry(trackinfo).State = System.Data.Entity.EntityState.Modified;

                //commit the transaction 
                context.SaveChanges();
            }
        }

        //the delete is an overloaded method tecnique
        [DataObjectMethod(DataObjectMethodType.Delete,true)]
        public void DeleteTrack(Track trackinfo)
        {
            DeleteTrack(trackinfo.TrackId);
        }

        public void DeleteTrack(int trackid)
        {
            using (var context = new ChinookContext())
            {
                //any business rules

                //do the delete
                //find the exising record on the database
                var existing = context.Tracks.Find(trackid);
                //delete the record from the database
                context.Tracks.Remove(existing);
                //commit the transaction 
                context.SaveChanges();
            }
        }
    }
}
