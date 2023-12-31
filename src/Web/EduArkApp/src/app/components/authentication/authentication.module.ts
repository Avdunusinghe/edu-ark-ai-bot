import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { AuthenticationRoutingModule } from './authentication-routing.module';
import { SignUpComponent } from './sign-up/sign-up.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FeatherModule } from 'angular-feather';
import { Facebook, Twitter, Github, Gitlab, User, Key, UserCheck, Mail, Home } from 'angular-feather/icons';

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
	declarations: [SignUpComponent, SignInComponent],
	imports: [
		CommonModule,
		AuthenticationRoutingModule,
		InputTextModule,
		FormsModule,
		ReactiveFormsModule,
		FeatherModule,
	],
})
export class AuthenticationModule {}
