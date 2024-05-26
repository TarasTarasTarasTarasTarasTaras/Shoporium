import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { StaticDataService } from 'src/app/modules/core/services/static-data.service';
import { AccountService } from '../../services/account.service';
import { forkJoin, switchMap } from 'rxjs';
import { NovaPostApiService } from 'src/app/modules/core/services/nova-post-api.service';

@Component({
  selector: 'app-my-address-data',
  templateUrl: './my-address-data.component.html',
  styleUrls: ['./my-address-data.component.css']
})
export class MyAddressDataComponent implements OnInit {
  cities = [];
  innerCities = [];
  selectInnerCities = [];
  postOffices = [];

  cityId: number;
  innerCityId: number;

  form = new FormGroup({
    cityId: new FormControl(0, Validators.required),
    innerCityId: new FormControl(0, Validators.required)
  });

  constructor(
    private accountService: AccountService,
    private novaPostApiService: NovaPostApiService,
    private staticDataService: StaticDataService) { }

    ngOnInit() {
      forkJoin({
        cities: this.staticDataService.getCities(),
        innerCities: this.staticDataService.getInnerCities()
      }).pipe(
        switchMap(results => {
          this.cities = results.cities;
          this.innerCities = results.innerCities;
          return this.accountService.getUserCity();
        })
      ).subscribe(innerCityId => {
        this.innerCityId = innerCityId;
        this.cityId = this.innerCities.find(c => c.id == innerCityId).regionId;

        this.form.controls['cityId'].setValue(this.cityId);
        this.onRegionChange();

        this.form.controls['innerCityId'].setValue(this.innerCityId);
        this.onCityNameChange();
      });
    }

  save() {
    this.accountService.updateUserCity(this.form.value.innerCityId).subscribe();
  }

  onRegionChange() {
    const cityId = this.form.value.cityId;
    this.selectInnerCities = this.innerCities.filter(c => c.regionId == cityId);
    this.postOffices = [];
  }

  onCityNameChange() {
    const cityName = this.selectInnerCities.find(c => c.id == this.form.value.innerCityId).name;
    this.novaPostApiService.getPostOffices(cityName).subscribe((res: any[]) => {
      this.postOffices = res;
    })
  }
}
