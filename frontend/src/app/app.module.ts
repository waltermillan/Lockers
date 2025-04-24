import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import {
  provideHttpClient,
  withInterceptorsFromDi,
  withFetch,
} from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { DatePipe } from '@angular/common';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { LockerCrudComponentComponent } from './lockers/locker-crud/locker-crud-component.component';
import { LockerUpdateComponent } from './lockers/locker-update/locker-update.component';
import { LocationCrudComponent } from './locations/location-crud/location-crud.component';
import { PriceCrudComponent } from './prices/price-crud/price-crud.component';
import { LocationUpdateComponent } from './locations/location-update/location-update.component';
import { PriceUpdateComponent } from './prices/price-update/price-update.component';
import { DocumentCrudComponent } from './documents/document-crud/document-crud.component';
import { DocumentUpdateComponent } from './documents/document-update/document-update.component';
import { CustomerCrudComponent } from './customers/customer-crud/customer-crud.component';
import { CustomerUpdateComponent } from './customers/customer-update/customer-update.component';
import { RoleCrudComponent } from './roles/role-crud/role-crud.component';
import { RoleUpdateComponent } from './roles/role-update/role-update.component';
import { RentCrudComponent } from './rents/rent-crud/rent-crud.component';
import { RentReadComponent } from './rents/rent-read/rent-read.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { SuccessDialogComponent } from './modals/success-dialog/success-dialog.component';
import { FailureDialogComponent } from './modals/failure-dialog/failure-dialog.component';
import { WarningDialogComponent } from './modals/warning-dialog/warning-dialog.component';
import { ConfirmDialogComponent } from './modals/confirm-dialog/confirm-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    LockerCrudComponentComponent,
    LockerUpdateComponent,
    LocationCrudComponent,
    PriceCrudComponent,
    LocationUpdateComponent,
    PriceUpdateComponent,
    DocumentCrudComponent,
    DocumentUpdateComponent,
    CustomerCrudComponent,
    CustomerUpdateComponent,
    RoleCrudComponent,
    RoleUpdateComponent,
    RentCrudComponent,
    RentReadComponent,
    SuccessDialogComponent,
    FailureDialogComponent,
    WarningDialogComponent,
    ConfirmDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    MatDialogModule,
  ],
  providers: [
    DatePipe,
    provideHttpClient(withInterceptorsFromDi(), withFetch()),
    provideAnimationsAsync(),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
