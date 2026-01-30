namespace DataMusic.Internal.Ports;

public interface IPortsGenericRepo<T> where T : class
{
  Task<string> Save(T info);
  Task<T?> GetById(string id);
  Task<T?> GetByName(string name);
  Task Edit(string id, T info);
  Task DeleteById(string id);
  Task DeleteByName(string name);
}