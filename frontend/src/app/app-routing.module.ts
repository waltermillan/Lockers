import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { LockerCrudComponentComponent } from './locker-crud/locker-crud-component.component';
import { LockerUpdateComponent } from './locker-update/locker-update.component';
import { LocationCrudComponent }  from './location-crud/location-crud.component';
import { PriceCrudComponent }  from './price-crud/price-crud.component';
import { LocationUpdateComponent }  from './location-update/location-update.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'Lockers', component: LockerCrudComponentComponent },
  { path: 'Locker-Update', component: LockerUpdateComponent },
  { path: 'Locations', component: LocationCrudComponent },
  { path: 'Prices', component: PriceCrudComponent },
  { path: 'Location-Update', component: LocationUpdateComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
