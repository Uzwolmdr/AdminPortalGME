import { useState, useEffect } from 'react';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer, Cell } from 'recharts';
import './Dashboard.css';

const Dashboard = () => {
  const [mounted, setMounted] = useState(false);

  useEffect(() => {
    setMounted(true);
  }, []);
  // Notification data
  const notification = {
    title: "GME Announces First Quarter 2025 Results",
    date: "Apr 02 2025 10:56 AM",
    description: "GME will host a conference call and earnings webcast at 2:00 p.m. Pacific Time/5:00 p.m. Eastern Time today to discuss these financial results. To register to participate in the conference call, please visit the Events & Presentations section of GME's Investor Relations website at investors.gme.com",
    url: "investors.gme.com"
  };

  // Overview metrics
  const metrics = [
    { label: 'Send', value: '22,234', color: 'green', icon: 'money' },
    { label: 'Pay', value: '6,373', color: 'blue', icon: 'airplane' },
    { label: 'Cancel', value: '133', color: 'red', icon: 'cancel' }
  ];

  // Chart data
  const chartData = [
    { name: 'CancelRequest', value: 200, color: '#10b981' },
    { name: 'CancelRequested', value: 670, color: '#3b82f6' },
    { name: 'Compliance', value: 250, color: '#f97316' },
    { name: 'Hold', value: 150, color: '#84cc16' },
    { name: 'ModificationRequest', value: 370, color: '#a855f7' },
    { name: 'OFAC', value: 130, color: '#059669' },
    { name: 'Post_Start', value: 130, color: '#ef4444' },
    { name: 'Pending GIBL Reprocessing', value: 450, color: '#3b82f6' }
  ];

  if (!mounted) {
    return <div className="dashboard-page">Loading...</div>;
  }

  return (
    <div className="dashboard-page">
      {/* Notifications Section */}
      <div className="dashboard-section">
        <div className="section-header">
          <h2 className="section-title">Notifications</h2>
          <a href="#" className="view-all-link">View All Notifications</a>
        </div>
        <div className="notification-card">
          <div className="notification-content">
            <div className="notification-header">
              <div className="notification-logo-title">
                <div className="gme-logo-small">
                  <img src="/GME-LOGO-HD.png" alt="GME Logo" className="gme-logo-image-small" />
                </div>
                <h3 className="notification-title">{notification.title}</h3>
              </div>
              <span className="notification-date">{notification.date}</span>
            </div>
            <p className="notification-description">
              {notification.description.split(notification.url)[0]}
              <a href={`https://${notification.url}`} className="notification-link" target="_blank" rel="noopener noreferrer">
                {notification.url}
              </a>
              {notification.description.split(notification.url)[1]}
            </p>
          </div>
        </div>
      </div>

      {/* Overview Section */}
      <div className="dashboard-section">
        <div className="section-header">
          <h2 className="section-title">Overview</h2>
          <button className="today-button">
            <svg className="calendar-icon" width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M3 2H4V1H5V2H11V1H12V2H13C13.5523 2 14 2.44772 14 3V13C14 13.5523 13.5523 14 13 14H3C2.44772 14 2 13.5523 2 13V3C2 2.44772 2.44772 2 3 2ZM3 4V13H13V4H3Z" fill="currentColor"/>
              <path d="M5 6H7V8H5V6Z" fill="currentColor"/>
              <path d="M9 6H11V8H9V6Z" fill="currentColor"/>
              <path d="M5 10H7V12H5V10Z" fill="currentColor"/>
            </svg>
            Today
          </button>
        </div>
        <div className="metrics-grid">
          {metrics.map((metric, index) => (
            <div key={index} className={`metric-card metric-${metric.color}`}>
              <div className="metric-content">
                <div className="metric-icon-wrapper">
                  {metric.icon === 'money' && (
                    <svg className="metric-icon" width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
                      <path d="M16 4C9.37258 4 4 9.37258 4 16C4 22.6274 9.37258 28 16 28C22.6274 28 28 22.6274 28 16C28 9.37258 22.6274 4 16 4ZM16 6C21.5228 6 26 10.4772 26 16C26 21.5228 21.5228 26 16 26C10.4772 26 6 21.5228 6 16C6 10.4772 10.4772 6 16 6Z" fill="currentColor"/>
                      <path d="M16 10V12H14V14H16V16H14V18H16V20H18V18H20V16H18V14H20V12H18V10H16ZM16 14V12H18V14H16Z" fill="currentColor"/>
                    </svg>
                  )}
                  {metric.icon === 'airplane' && (
                    <svg className="metric-icon" width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
                      <path d="M16 8L20 12L24 8L20 16L28 20L20 24L16 28L12 24L4 20L12 16L8 12L12 8L16 12L16 8Z" fill="currentColor"/>
                    </svg>
                  )}
                  {metric.icon === 'cancel' && (
                    <svg className="metric-icon" width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
                      <path d="M16 4C9.37258 4 4 9.37258 4 16C4 22.6274 9.37258 28 16 28C22.6274 28 28 22.6274 28 16C28 9.37258 22.6274 4 16 4ZM16 6C21.5228 6 26 10.4772 26 16C26 21.5228 21.5228 26 16 26C10.4772 26 6 21.5228 6 16C6 10.4772 10.4772 6 16 6Z" fill="currentColor"/>
                      <path d="M12 12L20 20M20 12L12 20" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
                    </svg>
                  )}
                </div>
                <div className="metric-info">
                  <div className="metric-value">{metric.value}</div>
                  <div className="metric-label">{metric.label}</div>
                </div>
              </div>
              <a href="#" className="metric-details-link">Details →</a>
            </div>
          ))}
        </div>
      </div>

      {/* Status Wise Transaction Chart */}
      <div className="dashboard-section">
        <div className="section-header">
          <h2 className="section-title">Status Wise Number of Transaction</h2>
        </div>
        <div className="chart-container">
          <ResponsiveContainer width="100%" height={400}>
            <BarChart data={chartData} margin={{ top: 20, right: 30, left: 20, bottom: 60 }}>
              <CartesianGrid strokeDasharray="3 3" stroke="#e5e7eb" />
              <XAxis 
                dataKey="name" 
                angle={-45}
                textAnchor="end"
                height={120}
                tick={{ fontSize: 12, fill: '#6b7280' }}
                interval={0}
              />
              <YAxis 
                domain={[0, 900]}
                ticks={[0, 100, 200, 300, 400, 500, 600, 700, 800, 900]}
                tick={{ fontSize: 12, fill: '#6b7280' }}
              />
              <Tooltip 
                contentStyle={{ 
                  backgroundColor: '#ffffff', 
                  border: '1px solid #e5e7eb',
                  borderRadius: '8px'
                }}
              />
              <Bar 
                dataKey="value" 
                radius={[8, 8, 0, 0]}
              >
                {chartData.map((entry, index) => (
                  <Cell key={`cell-${index}`} fill={entry.color} />
                ))}
              </Bar>
            </BarChart>
          </ResponsiveContainer>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;

