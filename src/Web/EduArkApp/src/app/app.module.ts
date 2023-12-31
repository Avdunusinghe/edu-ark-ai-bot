import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { CoreModule } from './core/core.module';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { fakeBackendProvider } from './core/interceptor/fake-backend';
import { ErrorInterceptor } from './core/interceptor/error.interceptor';
import { JwtInterceptor } from './core/interceptor/jwt.interceptor';
import {
	PerfectScrollbarModule,
	PERFECT_SCROLLBAR_CONFIG,
	PerfectScrollbarConfigInterface,
} from 'ngx-perfect-scrollbar';
import { SharedModule } from './components/shared/shared.module';

import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { LoadingBarRouterModule } from '@ngx-loading-bar/router';

import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { RightSidebarComponent } from './components/layout/right-sidebar/right-sidebar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HeaderComponent } from './components/layout/header/header.component';
import { SidebarComponent } from './components/layout/sidebar/sidebar.component';
import { AuthLayoutComponent } from './components/layout/app-layout/auth-layout/auth-layout.component';
import { MainLayoutComponent } from './components/layout/app-layout/main-layout/main-layout.component';
import { FooterComponent } from './components/layout/footer/footer.component';
import { PageLoaderComponent } from './components/layout/page-loader/page-loader.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule } from 'ngx-toastr';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
	wheelPropagation: false,
};

export function createTranslateLoader(http: HttpClient): any {
	return new TranslateHttpLoader(http, 'assets/i18n/', '.json');
}

@NgModule({
	declarations: [
		AppComponent,
		HeaderComponent,
		PageLoaderComponent,
		SidebarComponent,
		RightSidebarComponent,
		AuthLayoutComponent,
		MainLayoutComponent,
		FooterComponent,
	],
	imports: [
		BrowserModule,
		BrowserAnimationsModule,
		AppRoutingModule,
		HttpClientModule,
		ReactiveFormsModule,
		PerfectScrollbarModule,
		LoadingBarRouterModule,
		NgxSpinnerModule,
		TranslateModule.forRoot({
			loader: {
				provide: TranslateLoader,
				useFactory: createTranslateLoader,
				deps: [HttpClient],
			},
		}),
		// core & shared
		CoreModule,
		SharedModule,
		NgbModule,
		ToastrModule.forRoot(),
	],
	providers: [
		{ provide: LocationStrategy, useClass: HashLocationStrategy },
		{
			provide: PERFECT_SCROLLBAR_CONFIG,
			useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG,
		},
		{ provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
		{ provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
		fakeBackendProvider,
	],
	bootstrap: [AppComponent],
})
export class AppModule {}
