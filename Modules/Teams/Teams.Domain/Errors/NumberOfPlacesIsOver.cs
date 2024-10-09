using FluentResults;

namespace Teams.Domain.Errors;

public class NumberOfPlacesIsOver: Error
{
    public NumberOfPlacesIsOver()
    {
        Message = "The number of places for this invitation is over";
    }
}