import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Role } from '../models/role.models';
import { RoleService } from '../services/role.service';

@Component({
  selector: 'app-role-update',
  templateUrl: './role-update.component.html',
  styleUrl: './role-update.component.css'
})
export class RoleUpdateComponent implements OnInit {
  @Input() role: Role | null = null; // Receives the role from the parent
  @Output() close = new EventEmitter<void>();

  updRole:Role = {
    id: 0,
    description: ''
  }

  roles: Role[] = [];

  constructor(private roleService: RoleService) 
  {
  }

  ngOnInit(): void {
    this.getRoleById();
    this.getAllRoles();
  }

  onSubmit(){
    this.updateRole();
  }

  getRoleById()
  {
    if(this.role){
      this.updRole.id = this.role.id;
      this.updRole.description = this.role.description;
    }
  }

  getAllRoles():void{
    this.roleService.getAllRoles().subscribe({
      next: (data) => {
        this.roles = data;
      },
      error: (error) => {
        console.error('Error loading roles.');
      }
    });
  }

  updateRole() {
    console.log('Updating role', this.role);
    this.roleService.updateRole(this.updRole).subscribe({
      next: (data) => {
        alert('Role updated successfully.');
        console.log('Role updated successfully.');
      },
      error: (error) => {
        if(error.status === 400)
          alert('the role already exists!');
        console.error('Error updating new role', error);
      }
    })
  }

  closePopup() {
    this.close.emit();
  }
}
