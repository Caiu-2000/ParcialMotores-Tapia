public interface IObservable <T> 
{
    void Suscribe(IObserver<T> observer);
    void UnSuscribe(IObserver<T> observer);
}








