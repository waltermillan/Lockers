// src/app/services/dialog.service.ts

import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { firstValueFrom } from 'rxjs';

import { SuccessDialogComponent } from '@modals/success-dialog/success-dialog.component';
import { FailureDialogComponent } from '@modals/failure-dialog/failure-dialog.component';
import { WarningDialogComponent } from '@modals/warning-dialog/warning-dialog.component';
import { ConfirmDialogComponent } from '@modals/confirm-dialog/confirm-dialog.component';

import { DialogType } from 'enums/dialog-type.enum';

@Injectable({
  providedIn: 'root',
})
export class DialogService {
  /*
     Service creation: DialogService that acts as a facade encapsulating the Angular Material dialog 
     opening logic. It hides the implementation details of MatDialog. There is also a use of the 
     Factory pattern, since: The open() method decides which component to instantiate (Success, Warning, Failure)
     based on the type (DialogType), which is a basic form of Factory. Advantages: Reusable, Maintainable and 
     Decoupled.
  */

  constructor(private dialog: MatDialog) {}

  open(message: string, type: DialogType) {
    let component;

    switch (type) {
      case DialogType.Success:
        component = SuccessDialogComponent;
        break;
      case DialogType.Failure:
        component = FailureDialogComponent;
        break;
      case DialogType.Warning:
        component = WarningDialogComponent;
        break;
      default:
        throw new Error(`Dialog type '${type}' is not supported.`);
    }

    return this.dialog.open(component, {
      data: { message },
    });
  }

  confirm(
    message: string = 'Are you sure?',
    title: string = 'Confirma'
  ): Promise<boolean> {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '300px',
      data: { title, message },
    });

    return firstValueFrom(dialogRef.afterClosed());
  }
}
