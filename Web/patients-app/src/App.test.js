import { render, screen, within } from '@testing-library/react';
import App from './App';

const { stringContaining } = expect


describe('App', () => {

  beforeEach(() => {
    fetch.resetMocks()
    fetch.mockResponse('[]')
  })

  it('shows no clinic tabs initially', () => {
    render(<App />)
    const fragment = screen.getByRole("placeholder")
    expect(within(fragment).queryByRole('listitem'))
      .not.toBeInTheDocument()
  })

  // it('shows no clinic tabs after loading zero clinics', () => {
  //   fetch.mockResponse('[]')
  //   render(<App />)
  // })

  // it('shows one clinic tab after loading one clinic', async () => {
  //   fetch.mockResponse(JSON.stringify([ { name: 'Test Clinic 1' } ]))
  //   render(<App />)
  // })

  // it('shows many clinic tabs after loading many clinics', async () => {
  //   fetch.mockResponse(JSON.stringify(
  //     [ { name: 'Clinic 1' }, { name: 'Clinic 2' } ]
  //   ))
  //   render(<App />)
  // })
})
