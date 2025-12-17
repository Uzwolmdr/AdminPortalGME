# AdminPortal

A modern React-based admin portal application built with Vite, featuring user authentication, dashboard analytics, profile management, and password management capabilities.

## Features

- 🔐 **User Authentication** - Secure login system with email and user code
- 📊 **Dashboard** - Analytics and data visualization using Recharts
- 👤 **Profile Management** - View and manage user profile information
- 🔑 **Password Management** - Change password functionality
- 🎨 **Modern UI** - Clean and responsive design with custom components
- 🚀 **Fast Development** - Built with Vite for lightning-fast HMR

## Tech Stack

- **React** ^19.2.0 - UI library
- **React Router DOM** ^7.10.1 - Client-side routing
- **Vite** ^7.2.4 - Build tool and dev server
- **Recharts** ^3.5.1 - Chart library for data visualization
- **ESLint** - Code linting and quality

## Project Structure

```
AdminPortal/
├── vite-project/          # Main React application
│   ├── src/
│   │   ├── components/    # Reusable UI components
│   │   │   ├── AvatarDropdown.jsx
│   │   │   ├── DashboardFooter.jsx
│   │   │   ├── Header.jsx
│   │   │   ├── Layout.jsx
│   │   │   ├── Login.jsx
│   │   │   ├── LoginFooter.jsx
│   │   │   ├── Sidebar.jsx
│   │   │   └── VersionDisplay.jsx
│   │   ├── pages/          # Page components
│   │   │   ├── Dashboard.jsx
│   │   │   ├── Profile.jsx
│   │   │   └── ChangePassword.jsx
│   │   ├── services/      # API service layer
│   │   │   └── api.js
│   │   ├── App.jsx        # Main app component with routing
│   │   └── main.jsx       # Application entry point
│   ├── public/           # Static assets
│   ├── package.json
│   └── vite.config.js
└── README.md
```

## Getting Started

### Prerequisites

- Node.js (v16 or higher recommended)
- npm or yarn package manager

### Installation

1. Navigate to the vite-project directory:
   ```bash
   cd vite-project
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

### Development

Start the development server:

```bash
npm run dev
```

The application will be available at `http://localhost:5173` (or the next available port).

### Building for Production

Create a production build:

```bash
npm run build
```

The built files will be in the `dist` directory.

### Preview Production Build

Preview the production build locally:

```bash
npm run preview
```

### Linting

Run ESLint to check code quality:

```bash
npm run lint
```

## Configuration

### API Configuration

The application connects to a backend API. Configure the API base URL using environment variables:

Create a `.env` file in the `vite-project` directory:

```env
VITE_API_BASE_URL=http://localhost:5099/api
```

**Note:** 
- In development mode, the default is `http://localhost:5099/api`
- In production, it defaults to `/api` (relative path for same-domain deployment)

## Available Routes

- `/login` - Login page
- `/dashboard` - Main dashboard with analytics
- `/profile` - User profile page
- `/change-password` - Password change page

## API Endpoints

The application uses the following API endpoints:

- `POST /api/Login/LoginProcess` - User authentication
- `GET /api/Login/GetVersion` - Get application version
- `POST /api/Login/GetEmail` - Get user email by user code
- `POST /api/Login/ChangePassword` - Change user password

## Components Overview

### Layout Components
- **Layout** - Main layout wrapper with header and sidebar
- **Header** - Top navigation header
- **Sidebar** - Side navigation menu
- **DashboardFooter** - Footer component for dashboard pages
- **LoginFooter** - Footer component for login page

### Feature Components
- **Login** - Authentication form
- **AvatarDropdown** - User avatar with dropdown menu
- **VersionDisplay** - Application version display

### Pages
- **Dashboard** - Main dashboard with charts and analytics
- **Profile** - User profile information and management
- **ChangePassword** - Password change form

## Development Guidelines

- Follow React best practices and hooks patterns
- Use functional components with hooks
- Maintain consistent code style with ESLint
- Keep components modular and reusable
- Use the API service layer for all backend communications

## License

ISC

## Author

AdminPortal Development Team

