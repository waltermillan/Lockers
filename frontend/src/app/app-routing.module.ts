import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { LockerCrudComponentComponent } from './locker-crud/locker-crud-component.component';
import { LockerUpdateComponent } from './locker-update/locker-update.component';
import { LocationCrudComponent }  from './location-crud/location-crud.component';
import { LocationUpdateComponent }  from './location-update/location-update.component';
import { PriceCrudComponent }  from './price-crud/price-crud.component';
import { DocumentCrudComponent }  from './document-crud/document-crud.component';
import { DocumentUpdateComponent }  from './document-update/document-update.component';
import { CustomerCrudComponent }  from './customer-crud/customer-crud.component';
import { CustomerUpdateComponent }  from './customer-update/customer-update.component';
import { RoleCrudComponent }  from './role-crud/role-crud.component';
import { RoleUpdateComponent }  from './role-update/role-update.component';
import { RentCrudComponent }  from './rent-crud/rent-crud.component';
import { RentReadComponent }  from './rent-read/rent-read.component';

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
  exports: [RouterModule]
})
export class AppRoutingModule { }
