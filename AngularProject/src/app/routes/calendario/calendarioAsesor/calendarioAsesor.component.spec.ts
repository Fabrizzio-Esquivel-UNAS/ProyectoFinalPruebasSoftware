import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CalendarioAsesorComponent } from './calendarioAsesor.component';

describe('CalendarioAsesorComponent', () => {
  let component: CalendarioAsesorComponent;
  let fixture: ComponentFixture<CalendarioAsesorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CalendarioAsesorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CalendarioAsesorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
