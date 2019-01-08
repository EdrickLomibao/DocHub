using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using DocHub.Services;
using DocHub.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace DocHub.Hubs
{
    public class EditorHub : Hub
    {
        private DocumentService documentService;

        public EditorHub() : this( new DocumentService(new UnitOfWork()))
        {}

        public EditorHub(DocumentService _documentService)
        {
            documentService = _documentService;
        }

        public void UpdateContent(int ID, string name, string text)
        {
            documentService.Update(ID, name, text);

            Clients.All.updateEditorContent(text);

        }

    }
}