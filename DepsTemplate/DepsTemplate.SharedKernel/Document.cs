using DepsTemplate.SharedKernel.Interfaces;
using MongoDB.Bson;
using System;

namespace DepsTemplate.SharedKernel
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
