import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReporteCitasEstadoComponent } from './reporte-citas-estado.component';

describe('ReporteCitasEstadoComponent', () => {
  let component: ReporteCitasEstadoComponent;
  let fixture: ComponentFixture<ReporteCitasEstadoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ReporteCitasEstadoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReporteCitasEstadoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
