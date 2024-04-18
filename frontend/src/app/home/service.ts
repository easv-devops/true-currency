import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {firstValueFrom} from "rxjs";
import {EdgeFeatureHubConfig} from "featurehub-javascript-client-sdk";

@Injectable()
export class Service {
  constructor(private readonly http: HttpClient) {
  }

  async getCurrencies() {
    const call = this.http.get<Currency[]>("http://4.231.252.47:5002/Currency/GetAll");
    return await firstValueFrom<Currency[]>(call);
  }
  async getHistory() {
    const call = this.http.get<CurrencyHistory[]>("http://localhost:5002/Currency/GetAllHistory");
    return await firstValueFrom<CurrencyHistory[]>(call);
  }


  createHistory(historyRecord: CurrencyHistory) {
    this.http.post("http://localhost:5002/Currency/CreateHistory", historyRecord)
        .toPromise()
        .then(() => {
          console.log('History record created successfully');
        })
        .catch(error => {
          console.error('Failed to create history record:', error);
          throw error;
        });
  }

  async isFeatureEnabled(FEATURE_KEY: string) {
    const edgeUrl = 'http://localhost:8085/'; // for SaaS version, replace with edge URL provided in the API Keys page
    //TODO: Hide apiKey
    const apiKey = '5c0f0b36-21ed-4da1-bb6c-2ef1316ea865/J3tcF5V9eHZBwrw9IVgOaHMTDthmnCZi6claDzSw';

    const fhConfig = new EdgeFeatureHubConfig(edgeUrl, apiKey);

    fhConfig.init();
    const fhClient = await fhConfig.newContext().build();

    return fhClient.getFlag(FEATURE_KEY)
  }
}



export interface Currency{
  iso: string,
  rateToUsd: number
}

export interface CurrencyHistory {
  date: Date,
  source: string,
  target: string,
  value: number,
  result: number
}
