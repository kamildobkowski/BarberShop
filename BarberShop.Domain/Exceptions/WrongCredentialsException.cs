namespace BarberShop.Domain.Exceptions;

public class WrongCredentialsException : Exception
{
	public WrongCredentialsException(string message) : base(message) { }
}