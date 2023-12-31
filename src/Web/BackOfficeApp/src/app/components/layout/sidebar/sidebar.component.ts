import { Router, NavigationEnd } from '@angular/router';
import { DOCUMENT } from '@angular/common';
import { Component, Inject, ElementRef, OnInit, Renderer2, HostListener, OnDestroy } from '@angular/core';
import { ROUTES } from './sidebar-items';
import { AuthenticationService } from 'src/app/core/service/authentication.service';
import { AuthenticationResponseModel } from 'src/app/core/models/authentication/authentication.response.model';
@Component({
	selector: 'app-sidebar',
	templateUrl: './sidebar.component.html',
	styleUrls: ['./sidebar.component.sass'],
})
export class SidebarComponent implements OnInit, OnDestroy {
	public sidebarItems: any[];
	public innerHeight: any;
	public bodyTag: any;
	listMaxHeight: string;
	listMaxWidth: string;
	userFullName: string;
	userImg: string;
	userType: string;
	headerHeight = 60;
	routerObj = null;
	currentRoute: string;
	currentUser: AuthenticationResponseModel;
	constructor(
		@Inject(DOCUMENT) private document: Document,
		private renderer: Renderer2,
		public elementRef: ElementRef,
		private authService: AuthenticationService,
		private router: Router
	) {
		this.routerObj = this.router.events.subscribe((event) => {
			if (event instanceof NavigationEnd) {
				// close sidebar on mobile screen after menu select
				this.renderer.removeClass(this.document.body, 'overlay-open');
				this.sidebbarClose();
			}
		});

		this.currentUser = this.authService.currentUserValue;
	}
	@HostListener('window:resize', ['$event'])
	windowResizecall(event) {
		if (window.innerWidth < 1025) {
			this.renderer.removeClass(this.document.body, 'side-closed');
		}
		this.setMenuHeight();
		this.checkStatuForResize(false);
	}
	@HostListener('document:mousedown', ['$event'])
	onGlobalClick(event): void {
		if (!this.elementRef.nativeElement.contains(event.target)) {
			this.renderer.removeClass(this.document.body, 'overlay-open');
			this.sidebbarClose();
		}
	}
	callToggleMenu(event: any, length: any) {
		if (length > 0) {
			const parentElement = event.target.closest('li');
			const activeClass = parentElement.classList.contains('active');

			if (activeClass) {
				this.renderer.removeClass(parentElement, 'active');
			} else {
				this.renderer.addClass(parentElement, 'active');
			}
		}
	}
	ngOnInit() {
		if (this.authService.currentUserValue) {
			this.sidebarItems = ROUTES.filter((sidebarItem) => sidebarItem);
		}
		this.initLeftSidebar();
		this.bodyTag = this.document.body;
	}
	ngOnDestroy() {
		this.routerObj.unsubscribe();
	}
	initLeftSidebar() {
		const _this = this;
		// Set menu height
		_this.setMenuHeight();
		_this.checkStatuForResize(true);
	}
	setMenuHeight() {
		this.innerHeight = window.innerHeight;
		const height = this.innerHeight - this.headerHeight;
		this.listMaxHeight = height + '';
		this.listMaxWidth = '500px';
	}
	isOpen() {
		return this.bodyTag.classList.contains('overlay-open');
	}
	checkStatuForResize(firstTime) {
		if (window.innerWidth < 1025) {
			this.renderer.addClass(this.document.body, 'sidebar-gone');
		} else {
			this.renderer.removeClass(this.document.body, 'sidebar-gone');
		}
	}
	mouseHover(e) {
		const body = this.elementRef.nativeElement.closest('body');
		if (body.classList.contains('submenu-closed')) {
			this.renderer.addClass(this.document.body, 'side-closed-hover');
			this.renderer.removeClass(this.document.body, 'submenu-closed');
		}
	}
	mouseOut(e) {
		const body = this.elementRef.nativeElement.closest('body');
		if (body.classList.contains('side-closed-hover')) {
			this.renderer.removeClass(this.document.body, 'side-closed-hover');
			this.renderer.addClass(this.document.body, 'submenu-closed');
		}
	}

	sidebbarClose() {
		if (window.innerWidth < 1025) {
			this.renderer.addClass(this.document.body, 'sidebar-gone');
		}
	}
}
