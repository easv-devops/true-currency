import {Component, OnInit} from '@angular/core';
import {FormBuilder, Validators} from "@angular/forms";
import {Currency, CurrencyHistory, Service} from "./service";

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {
  protected readonly Number = Number;
  result?: number;
  indexes?: Currency[];
  cHistory?: CurrencyHistory[];

  constructor(private readonly fb: FormBuilder,
              private readonly service: Service) {
  }

  async ngOnInit() {
        this.indexes = await this.service.getCurrencies();
    this.cHistory = await this.service.getHistory();
    console.log(this.cHistory[0])
    }

  form = this.fb.group({
    source: ['', Validators.required],
    target: ['', Validators.required],
    value: ['', Validators.required]
  });

  get source() {
    return this.form.controls.source.value;
  }

  get target() {
    return this.form.controls.target.value;
  }

  get value() {
    return this.form.controls.value;
  }

  convert() {
    var source = this.form.controls.source.value;
    var target = this.form.controls.target.value;
    var value = this.form.controls.value.value;

    var sourceRateToUsd;
    var targetRateToUsd;

    for (const c of this.indexes!) {
      if(c.iso == source) {
        sourceRateToUsd = c.rateToUsd;
      }
      if(c.iso == target) {
        targetRateToUsd = c.rateToUsd;
      }
    }

    // @ts-ignore
    this.result = value?.valueOf() / sourceRateToUsd * targetRateToUsd;
  }

  createHistory() {
    console.log("fnuweinfw")

    const source = this.source;
    const target = this.target;
    const value = this.value;

    // Find ISO values for source and target currencies
    const sourceCurrency = this.indexes?.find(c => c.iso === source);
    const targetCurrency = this.indexes?.find(c => c.iso === target);

    if (!sourceCurrency || !targetCurrency) {
      console.error('Source or target currency not found');
      return;
    }

    const historyRecord: CurrencyHistory = {
      date: new Date(),
      source: sourceCurrency.iso,
      target: targetCurrency.iso,
      value: Number(this.form.controls.value.value),
      result: this.result ? Number(this.result) : 0
    };
    this.cHistory?.push(historyRecord)
      this.service.createHistory(historyRecord);
  }
}
