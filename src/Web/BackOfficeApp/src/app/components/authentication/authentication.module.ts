import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationRoutingModule } from './authentication-routing.module';
import { SignUpComponent } from './sign-up/sign-up.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Facebook, Twitter, Github, Gitlab, User, Key, UserCheck, Mail, Home } from 'angular-feather/icons';
import { FeatherModule } from 'angular-feather';

const icons = {
	Facebook,
	Twitter,
	Github,
	Gitlab,
	User,
	Key,
	UserCheck,
	Mail,
	Home,
};
@NgModule({
	declarations: [SignUpComponent],
	imports: [CommonModule, FormsModule, ReactiveFormsModule, AuthenticationRoutingModule, FeatherModule],
})
export class AuthenticationModule {}
