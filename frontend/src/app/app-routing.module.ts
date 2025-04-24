import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { LockerCrudComponentComponent } from './lockers/locker-crud/locker-crud-component.component';
import { LockerUpdateComponent } from './lockers/locker-update/locker-update.component';
import { LocationCrudComponent } from './locations/location-crud/location-crud.component';
import { LocationUpdateComponent } from './locations/location-update/location-update.component';
import { PriceCrudComponent } from './prices/price-crud/price-crud.component';
import { DocumentCrudComponent } from './documents/document-crud/document-crud.component';
import { DocumentUpdateComponent } from './documents/document-update/document-update.component';
import { CustomerCrudComponent } from './customers/customer-crud/customer-crud.component';
import { CustomerUpdateComponent } from './customers/customer-update/customer-update.component';
import { RoleCrudComponent } from './roles/role-crud/role-crud.component';
import { RoleUpdateComponent } from './roles/role-update/role-update.component';
import { RentCrudComponent } from './rents/rent-crud/rent-crud.component';
import { RentReadComponent } from './rents/rent-read/rent-read.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'Lockers', component: LockerCrudComponentComponent },
  { path: 'Locker-Update', component: LockerUpdateComponent },
  { path: 'Locations', component: LocationCrudComponent },
  { path: 'Location-Update', component: LocationUpdateComponent },
  { path: 'Prices', component: PriceCrudComponent },

  { path: 'Documents', component: DocumentCrudComponent },
  { path: 'Documents-Update', component: DocumentUpdateComponent },

  { path: 'Customers', component: CustomerCrudComponent },
  { path: 'Customers-Update', component: CustomerUpdateComponent },

  { path: 'Roles', component: RoleCrudComponent },
  { path: 'Roles-Update', component: RoleUpdateComponent },

  { path: 'NewRent', component: RentCrudComponent },
  { path: 'Rents', component: RentReadComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
