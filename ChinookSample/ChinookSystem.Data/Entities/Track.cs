﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace ChinookSystem.Data.Entities
{
    //point to the sql table that this file maps
    [Table("Tracks")]
    public class Track
    {
        //Key notations is optional if the sql pkey
        //ends in ID or Id
        //required if default of entity is NOT Identity
        //required if pkey is compound

        //properties can be fully implemented or
        //auto implemented
        //property names should use sql attribute name
        //properties should be listed in the same order
        //     as sql table attributes for easy of maintenance

            //entity validation
            //this is validation that kicks in when the 
            //.SaveChange() command is executed
            //[Required(Errormessage"xxx")]
            //[StringLength(int maximum[,int minimum],ErrorMessage="xxx")]
            //[Range(double minimum,double maximum, Errormessage="xxx")]
            //[RegularExpression("expression",ErrorMessage="xxx")]

        [Key]
        public int TrackId { get; set; }
        [Required(ErrorMessage ="Name is a Required field")]
        [StringLength(200, ErrorMessage ="Name is too long.Max Characters: 200")]
        public string Name { get; set; }
        [Range(1.0,double.MaxValue,ErrorMessage = "Inavalid Album,try selection again")]
        public int? AlbumId { get; set; }
        [Required(ErrorMessage = "MediaType is a Required field")]
        [Range(1.0, double.MaxValue, ErrorMessage = "Inavalid MediaType,try selection again")]
        public int MediaTypeId { get; set; }
        [Range(1.0, double.MaxValue, ErrorMessage = "Inavalid Genre,try selection again")]
        public int? GenreId { get; set; }
        [StringLength(220, ErrorMessage = "Composer is too long.Max Characters: 220")]
        public string Composer { get; set; }
        [Required(ErrorMessage = "MSec is a Required field")]
        [Range(1.0, double.MaxValue, ErrorMessage = "Inavalid Msec,Must be greaterthan 0")]
        public int Milliseconds { get; set; }
        [Range(1.0, double.MaxValue, ErrorMessage = "Inavalid Bytes,Must be greaterthan 0")]
        public int? Bytes { get; set; }
        [Required(ErrorMessage = "Price is a Required field")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Inavalid Price,Must be 0 or greater")]
        public decimal UnitPrice { get; set; }

        //navigation properties for use by Linq
        //these properties will be of type vitural
        //there are two types of navigation properties
        //properties that point to "children" use ICollection<T>
        //properties that point to "Parent" use ParentName as the datatype
        public virtual MediaType MediaType { get; set; }
        public virtual Album Album { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }

        //Tracks may be on one or more PlayList. Each PlayList has one or more Tracks
        //this many to many relationship was normalized using a table called PlaylistTracks
        //We can simplify our model by using navigation properties to directly
        //    represent our many-to-many relationship and thereby omit having to
        //    create a PlaylistTrack entity
        //The navigation property set would be as  "children" 

        //Modeling of this relationship will be done in the context class
        public virtual ICollection<Playlist> PlayLists { get; set; }
    }
}