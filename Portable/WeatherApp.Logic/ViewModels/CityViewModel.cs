﻿using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Logic.Models;
using WeatherApp.Logic.Services;

namespace WeatherApp.Logic.ViewModels
{
    public class CityViewModel
    {
        private readonly City _city;
        private readonly WeatherServiceAgent _weatherServiceAgent;

        private Observable<Exception> _lastError = new Observable<Exception>();
        
        public CityViewModel(City city, WeatherServiceAgent weatherServiceAgent)
        {
            _city = city;
            _weatherServiceAgent = weatherServiceAgent;
        }

        public string Name
        {
            get { return _city.Name; }
        }

        public IEnumerable<ForecastViewModel> Forecasts
        {
            get
            {
                return
                    from forecast in _city.Forecasts
                    select new ForecastViewModel(forecast);
            }
        }

        public async void Refresh()
        {
            try
            {
                _lastError.Value = null;
                await _weatherServiceAgent.Refresh();
            }
            catch (Exception ex)
            {
                _lastError.Value = ex;
            }
        }
    }
}