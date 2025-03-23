import { Component, OnInit } from '@angular/core';
import { Role } from '../models/role.models';
import { RoleService } from '../services/role.service';

@Component({
  selector: 'app-role-crud',
  templateUrl: './role-crud.component.html',
  styleUrl: './role-crud.component.css'
})
export class RoleCrudComponent implements OnInit{

  roles:Role[] = [];
  isPopupOpen:boolean = false;
  selectedRole: Role | null = null;

  newRole:Role = {
    id: 0,
    description: ''
  }

  constructor(private roleService: RoleService) {

  }

  ngOnInit(): void {
    this.getAllRoles();
  }

  onSubmit(){
    console.log(JSON.stringify(this.newRole));
    this.addRole();
  }

  addRole(){
      this.roleService.addRole(this.newRole).subscribe({
        next: (data) => {
          this.getAllRoles();
          alert('Role addedd successfully.');
          console.log('Role addedd successfully.');
        },
        error: (error) => {
          console.error('Error adding new role.',error)
        }
      });
  }

  getAllRoles(){
    this.roleService.getAllRoles().subscribe({
      next: (data) => {
        this.roles = data;
        console.log('Loading all roles.');
      },
      error: (error) => {
        console.error('Error adding new role.',error)
      }
    });
  }

  deleteRole(id:number){
    this.roleService.deleteRole(id).subscribe({
      next: (data) => {
        alert('Role deleted successfully.');
        console.log('Role deleted successfully.');
        this.getAllRoles();
      },
      error: (error) => {
        console.error('Error deleting role', error);
      }
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
