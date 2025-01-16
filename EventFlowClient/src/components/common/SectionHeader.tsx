interface SectionHeaderProps {
  title: string;
  subtitle?: string;
}
const SectionHeader = ({ title, subtitle }: SectionHeaderProps) => {
  return (
    <article className="flex flex-col justify-center items-center gap-4">
      <h2 className="border-b-primaryPurple pb-1 border-b-4 px-6 text-center">{title}</h2>
      <p className="text-textPrimary">{subtitle}</p>
    </article>
  );
};
export default SectionHeader;
