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
}

export interface Currency{
  iso: string,
  rateToUsd: number
}
