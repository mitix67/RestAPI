namespace RestAPI
{
    public interface IWeatherForecastServicee
    {
        IEnumerable<WeatherForecast> Get();
    }
}
