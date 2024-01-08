namespace RestAPI
{
    public interface IWeatherForecastServicee
    {
        IEnumerable<WeatherForecast> Get();
        IEnumerable<WeatherForecast> Post(int numberOfResults, int tempMin, int tempMax);
    }
}
