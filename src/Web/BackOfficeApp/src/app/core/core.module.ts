import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RightSidebarService } from './service/rightsidebar.service';
import { AuthGuard } from './guard/auth.guard';
import { DynamicScriptLoaderService } from './service/dynamic-script-loader.service';
import { throwIfAlreadyLoaded } from './guard/module-import.guard';
import { AuthenticationService } from './service/authentication.service';

@NgModule({
	declarations: [],
	imports: [CommonModule],
	providers: [RightSidebarService, AuthGuard, AuthenticationService, DynamicScriptLoaderService],
})
export class CoreModule {
	constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
		throwIfAlreadyLoaded(parentModule, 'CoreModule');
	}
}
