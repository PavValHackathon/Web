namespace PavValHackathon.Web.Common.Mappers
{
    public interface IMapperDefinition
    { }

    public interface IMapperDefinition<in TFrom, TTo> : IMapperDefinition
    {
        TTo Map(TFrom from);
        TTo Map(TTo to, TFrom from);
    }
}