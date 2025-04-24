import { Component, OnInit } from '@angular/core';
import { Role } from '@models/role.models';
import { RoleService } from '@services/role.service';
import { DialogType } from 'enums/dialog-type.enum';
import { DialogService } from '@services/dialog-service.service';

@Component({
  selector: 'app-role-crud',
  templateUrl: './role-crud.component.html',
  styleUrl: './role-crud.component.css',
})
export class RoleCrudComponent implements OnInit {
  roles: Role[] = [];
  isPopupOpen: boolean = false;
  selectedRole: Role | null = null;

  newRole: Role = {
    id: 0,
    description: '',
  };

  constructor(
    private roleService: RoleService,
    private dialogService: DialogService
  ) {}

  ngOnInit(): void {
    this.getAllRoles();
  }

  onSubmit() {
    console.log(JSON.stringify(this.newRole));
    this.addRole();
  }

  addRole() {
    this.roleService.add(this.newRole).subscribe({
      next: (data) => {
        this.getAllRoles();
        this.dialogService.open(
          'Role addedd successfully.',
          DialogType.Success
        );
        console.log('Role addedd successfully.');
      },
      error: (error) => {
        this.dialogService.open('Error adding new role.', DialogType.Failure);
        console.error('Error adding new role.', error);
      },
    });
  }

  getAllRoles() {
    this.roleService.getAll().subscribe({
      next: (data) => {
        this.roles = data;
      },
      error: (error) => {
        console.error('Error getting roles.', error);
        this.dialogService.open('Error getting roles.', DialogType.Failure);
      },
    });
  }

  deleteRole(id: number) {
    this.roleService.delete(id).subscribe({
      next: (data) => {
        this.dialogService.open(
          'Role deleted successfully.',
          DialogType.Success
        );
        console.log('Role deleted successfully.');
        this.getAllRoles();
      },
      error: (error) => {
        console.error('Error deleting role.', error);
        this.dialogService.open('Error deleting role.', DialogType.Failure);
      },
    });
  }

  updateRole(role: Role) {
    this.selectedRole = role;
    this.isPopupOpen = true;
  }

  closePopup() {
    this.isPopupOpen = false;
    this.selectedRole = null;
    this.getAllRoles();
  }
}
