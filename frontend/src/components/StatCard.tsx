interface StatCardProps {
  title: string;
  value: number;
}

function StatCard({ title, value }: StatCardProps) {
  return (
    <div className="stat-card">
      <span className="stat-title">{title}</span>
      <span className="stat-value">{value}</span>
    </div>
  );
}

export default StatCard;