using Dodo.Core.DomainModel.Clients;

namespace Dodo.Core.Services
{
	public enum ConfirmationCodeDestination
	{
		/// <summary>
		/// Восстпновление пароля
		/// </summary>
		RecoveryPassword = 1,

		/// <summary>
		/// Изменение телефона
		/// </summary>
		PhoneChange = 2,

		/// <summary>
		/// Регистраци
		/// </summary>
		Registration = 3,

		Authentication = 4
	}

	public interface IClientsService
	{
		ClientIcon[] GetIcons();
	}
}