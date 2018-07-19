import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import TestAppService from '../../../shared/services/test-app-service';

@Component
export default class FetchDataComponent extends Vue {
    forecasts: IWeatherForecast[] = [];

    mounted() {
        let testAppService = new TestAppService();
        testAppService.getAll().then((response) => {
            this.forecasts = response.content as IWeatherForecast[];
        });
    }
}
