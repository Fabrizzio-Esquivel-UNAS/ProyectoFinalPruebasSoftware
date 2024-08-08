import { inject, Inject, OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';
import { TranslocoService } from '@jsverse/transloco';
import { AccountState } from '@state/account.state';
import { filter, take } from 'rxjs';
import { UserRole } from '@domain/user.model';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit {
    accountState = inject(AccountState);

    model: any[] = [];

    constructor(private translocoService: TranslocoService, public layoutService: LayoutService) { }

    private addItems(itemLabels: any, role: UserRole) {
        this.model = [{
            label: itemLabels['rutas.home'],
            items: [{ label: itemLabels['rutas.homepage'], icon: 'pi pi-fw pi-home', routerLink: ['/'] }]
        },{}];
        if (!(role==UserRole.Admin || role==UserRole.Coordinador)) {
            if ((role==UserRole.User)) {
                this.model[0].items.push({ label: itemLabels['rutas.solicitarAsesoria'], icon: 'pi pi-fw pi-send', routerLink: ['/solicitar'] });
            }
            if ((role==UserRole.User || role==UserRole.Asesorado || role==UserRole.Asesor)) {
                this.model[0].items.push({ label: itemLabels['rutas.solicitudes'], icon: 'pi pi-fw pi-inbox', routerLink: ['/solicitudes'] });
            }
            if ((role==UserRole.Asesorado || role==UserRole.Asesor)) {
                this.model[0].items.push({ label: itemLabels['rutas.citas'], icon: 'pi pi-fw pi-address-book', routerLink: ['/citas'] });
            }
            if ((role==UserRole.Asesor)) {
                this.model[0].items.push({ label: itemLabels['rutas.calendarioAsesor'], icon: 'pi pi-fw pi-calendar', routerLink: ['/calendarioAsesor'] });
            }
            if ((role==UserRole.Asesorado)) {
                this.model[0].items.push({ label: itemLabels['rutas.calendarioAsesorado'], icon: 'pi pi-fw pi-calendar-clock', routerLink: ['/calendarioAsesorado'] });
            }
        }

        if ((role==UserRole.Admin || role==UserRole.Coordinador)) {
            this.model[1] = {
                label: itemLabels['comandos.gestionar.verbo'],
                items: []
            };
            this.model[1].items.push({ label: itemLabels['entidades.usuario.plural'], icon: 'pi pi-fw pi-users', routerLink: ['/usuarios'] });

            if (!(role==UserRole.Admin))
                return
            this.model[1].items.push({ label: itemLabels['entidades.facultad.plural'], icon: 'pi pi-building-columns', routerLink: ['/facultades'] });
            this.model[1].items.push({ label: itemLabels['entidades.escuela.plural'], icon: 'pi pi-fw pi-graduation-cap', routerLink: ['/escuelas'] });
            this.model[1].items.push({ label: itemLabels['entidades.grupoInvestigacion.plural'], icon: 'pi pi-share-alt', routerLink: ['/gruposInvestigacion'] });
            this.model[1].items.push({ label: itemLabels['entidades.lineaInvestigacion.plural'], icon: 'pi pi-fw pi-search', routerLink: ['/lineasInvestigacion'] });
        }        
    }
    
    ngOnInit() {
        this.translocoService.selectTranslation()
            .subscribe(itemLabels => {
                this.addItems(itemLabels, UserRole.User);
                this.accountState.stateItem$.pipe(
                    filter(x => x!=null && x.role!=null),
                    take(1),
                ).subscribe(account => {
                    this.addItems(itemLabels, account?.role!);
                })
            });
    }
}
