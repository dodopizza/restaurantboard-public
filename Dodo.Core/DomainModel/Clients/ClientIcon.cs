using System;

namespace Dodo.Core.DomainModel.Clients
{
	public class ClientIcon
	{
		public Int32 Id { get; private set; }
		public String Path { get; private set; }

		public string GetUrl(String fileStorageHost)
		{
			return String.Concat(fileStorageHost, Path);
		}

		public ClientIcon(Int32 id, String path)
		{
			Path = path;
			Id = id;
		}
	}
}