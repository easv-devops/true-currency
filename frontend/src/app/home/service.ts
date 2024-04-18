import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {firstValueFrom} from "rxjs";

@Injectable()
export class Service {
  constructor(private readonly http: HttpClient) {
  }

  async getCurrencies() {
    const call = this.http.get<Currency[]>("http://4.231.252.47:5002/Currency/GetAll");
    return await firstValueFrom<Currency[]>(call);
  }
  async getHistory() {
    const call = this.http.get<CurrencyHistory[]>("http://localhost:5240/Currency/GetAllHistory");
    return await firstValueFrom<CurrencyHistory[]>(call);
  }


  createHistory(historyRecord: CurrencyHistory) {
    this.http.post("http://localhost:5240/Currency/CreateHistory", historyRecord)
        .toPromise()
        .then(() => {
          console.log('History record created successfully');
        })
        .catch(error => {
          console.error('Failed to create history record:', error);
          throw error;
        });
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
