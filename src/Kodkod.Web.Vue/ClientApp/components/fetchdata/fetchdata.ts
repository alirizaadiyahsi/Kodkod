import Vue from 'vue';
import { Component } from 'vue-property-decorator';

interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

@Component
export default class FetchDataComponent extends Vue {
    forecasts: WeatherForecast[] = [];

    mounted() {
        fetch('http://localhost:60320/api/Test/WeatherForecasts')
            .then(response => response.json() as Promise<WeatherForecast[]>)
            .then(data => {
                this.forecasts = data;
            });
    }
}
